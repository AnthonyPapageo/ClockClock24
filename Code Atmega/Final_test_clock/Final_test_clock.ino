#include <SoftwareSerial.h>
#include <Wire.h>
#include <Arduino.h>
#include <Stepper.h>

//AccelStepper stepperBig (5, 8, 2, 10 );
//AccelStepper stepperSmall(6, 7, 4, 9);

void SerialRecieve (void);
void MoveClock(void);

Stepper myStepperBig(180, 6, 7, 4, 9);
Stepper myStepperSmall(180, 5, 8, 2, 10);
SoftwareSerial mySerial(16, 17); // RX, TX
int16_t tabAngle[65]; // tab[0] is number of clock 0 is big handle, 1 is small handle
int16_t tabOldAngle[2] = {0, 0};
int16_t startMarker = 369;





void setup()
{
  Serial.begin(115200);
  while(!Serial); //wait serial to be ready
  Wire.begin(); //Join I2C bus as master
  Wire.setClock(100000);
  myStepperBig.setSpeed(200);
  myStepperSmall.setSpeed(200);
  Serial.println("Arduino Launching");

 // myStepperBig.step(720); //720 = tour complet
  //myStepperSmall.step(360);

  mySerial.begin(115200);
  mySerial.println("Arduino launched");

}

void loop()
{
  uint8_t address; //adress counter
  uint8_t j = 3; //tab[3] is the first handle from first slave
  mySerial.println("Waiting for angles");
  mySerial.listen();
  
  while (!getBegin()); //wait for start condition
  myStepperBig.step(720); //debug
  myStepperSmall.step(360);
  mySerial.println("RECIEVED START");
  SerialRecieve();
  MoveClock(); //Move clock from Master
  for (address = 2; address <= tabAngle[0]; address++) //tab[0} is nb of clock
  {
    Wire.beginTransmission(address);
    for (j = 3; j <= (2 * address); j++)
    {
      Wire.write(tabAngle[j] >> 8); 
      Wire.write(tabAngle[j] & 0xff); 
      mySerial.print("SENT");
      mySerial.println(tabAngle[j]);
    }
    Wire.endTransmission();
  }

}

bool getBegin(void)
{
	int temp1, temp2;
  int val;
  //mySerial.print("waiting serial ");
  //Serial.print("waiting serial main");
	if(mySerial.available() > 1)
	{
		temp1 = mySerial.read() * 256;
		temp2 = mySerial.read();
    val = temp1 + temp2;
    Serial.print("VAL is ");
    Serial.println(val);
		if(val == startMarker )
		{
			return true;
		}
	}
 return false;
}







void SerialRecieve()
{
  int temp1, temp2;
  uint16_t endMarker = 420;
  uint8_t counter = 0;
  while (mySerial.available())
  {
    temp1 = mySerial.read()* 256; //MSB
    temp2 = mySerial.read();
    temp1 += temp2;
    Serial.print("recieved ");
    Serial.println(temp1);
    if (temp1 == endMarker) //end
    {
      Serial.println("END of reception");
      int i;
      for (i = 0; i < counter; i++)
      {
        Serial.println(tabAngle[i]);
      }
      break;
    }
	else
	{
		tabAngle[counter] = temp1;
		counter++;
	}
    
  }
}


void MoveClock(void)
{
  int16_t temp;
  temp = ((tabAngle[1] - tabOldAngle[0]) * 2); //2 step per deg
  myStepperBig.step(temp); //2 step per deg
  temp = ((tabAngle[2] - tabOldAngle[1]) * 2);
  myStepperSmall.step(temp);
  tabOldAngle[0] = tabAngle[1];
  tabOldAngle[1] = tabAngle[2];
}
