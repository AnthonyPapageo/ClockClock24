/*Begining of Auto generated code by Atmel studio */
#include <Arduino.h>
#include <Stepper.h>
#include <Wire.h>
#include <avr/io.h>
//Beginning of Auto generated function prototypes by Atmel Studio
uint8_t getAddress(void );
//End of Auto generated function prototypes by Atmel Studio


bool valueRecieved = false;
/* AccelStepper stepperBig (5, 8, 2, 10);
  AccelStepper stepperSmall(6, 7, 4, 9); */
Stepper myStepperBig(180, 6, 7, 4, 9); //BigHandle
Stepper myStepperSmall(180, 5, 8, 2, 10); //SmallHandle
volatile int16_t tabAngle[2]; //0 is big handle, 1 is small handle
int16_t tabOldAngle[2] = {0, 0}; //old values are stocked here





void setup()
{
  Serial.begin(115200);
  while (!Serial); //wait serial to be ready
  Serial.println("SLAVE LAUNCHING");
  DDRC |= (1 << 0) | (1 << 1); //Mux data selector pins as output
  Wire.begin(getAddress()); //set slave address
  myStepperBig.setSpeed(200);
  myStepperSmall.setSpeed(200);
  Wire.onReceive(recieveEvent); //read the angles

}

void loop()
{
  while (valueRecieved)
  {
    int16_t angleToDo1;
    int16_t angleToDo2;
    angleToDo1 = tabAngle[0] - tabOldAngle[0];
    tabOldAngle[0] = tabAngle[0]; //save new angle
    Serial.println("Caluclated value are");
    Serial.println(angleToDo1);
    myStepperBig.step(angleToDo1 * 2); //2 step per deg
    angleToDo2 = tabAngle[1] - tabOldAngle[1];
    tabOldAngle[1] = tabAngle[1];
    Serial.println(angleToDo2);
    myStepperSmall.step(angleToDo2 * 2);
    valueRecieved = false;
  }

}

uint8_t getAddress(void) //MuxB = AIN0 Mux C = AIN1  MuxA = ADC7
{
  uint8_t result = 2;// Mux not working, default address 2
  return result;
}


void recieveEvent(int numBytes)
{
  int16_t tab_temp[5]; //i2c transmit 8 bits data
  uint8_t i = 0;
  Serial.println("EVENT");
  while (1 < Wire.available())
  {
    tab_temp[i] = Wire.read();
    Serial.print("Recieved in the loop ");
    Serial.println(tab_temp[i]);
    i++;   
  }
  tab_temp[i] = Wire.read();
  Serial.println(tab_temp[i]);
  
  tabAngle[0] = (tab_temp[0] << 8) | (tab_temp[1]); //merge msb and lsb
  tabAngle[1] = (tab_temp[2] << 8) | (tab_temp[3]);
  Serial.print("the angles recieved are ");
  Serial.println(tabAngle[0]);
  Serial.println(tabAngle[1]);
  valueRecieved = true;
}
