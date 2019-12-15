#include <SoftwareSerial.h>
#include <Wire.h>

/*
  Software serial multple serial test

 Receives from the two software serial ports,
 sends to the hardware serial port.

 In order to listen on a software port, you call port.listen().
 When using two software serial ports, you have to switch ports
 by listen()ing on each one in turn. Pick a logical time to switch
 ports, like the end of an expected transmission, or when the
 buffer is empty. This example switches ports when there is nothing
 more to read from a port

 The circuit:
 Two devices which communicate serially are needed.
 * First serial device's TX attached to digital pin 10(RX), RX to pin 11(TX)
 * Second serial device's TX attached to digital pin 8(RX), RX to pin 9(TX)

 Note:
 Not all pins on the Mega and Mega 2560 support change interrupts,
 so only the following can be used for RX:
 10, 11, 12, 13, 50, 51, 52, 53, 62, 63, 64, 65, 66, 67, 68, 69

 Not all pins on the Leonardo support change interrupts,
 so only the following can be used for RX:
 8, 9, 10, 11, 14 (MISO), 15 (SCK), 16 (MOSI).

 created 18 Apr. 2011
 modified 19 March 2016
 by Tom Igoe
 based on Mikal Hart's twoPortRXExample

 This example code is in the public domain.

 */


// software serial #1: RX = digital pin 10, TX = digital pin 11

// software serial #2: RX = digital pin 8, TX = digital pin 9
// on the Mega, use other pins instead, since 8 and 9 don't work on the Mega
SoftwareSerial mySerial(16, 17); // RX, TX

void setup() {
  // Open serial communications and wait for port to open:
  Serial.begin(115200);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }


  // Start each software serial por
  mySerial.begin(115200);
  mySerial.print("hello from soft serial");
  Serial.print("hello from true serial");
}

void loop() 
{

  mySerial.listen();
  int temp1;
  int temp2;
  int val;
  while (mySerial.available() > 0) 
    {
      //val = Serial.parseInt();
       temp1 = mySerial.read() * 256;
       temp2 = mySerial.read();
       val = temp2  + (temp1);
      Serial.println(String(val));
      delay(1);
    }

     
  }
 
