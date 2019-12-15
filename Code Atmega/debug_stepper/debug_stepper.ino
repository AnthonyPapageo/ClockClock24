#include <AccelStepper.h>
#include <MultiStepper.h>

#include <Stepper.h>
//Stepper myStepperBig(180, 7, 4, 9, 6); //BigHandle
Stepper myStepperBig(180, 4, 6, 7, 9); //BigHandle
//Stepper myStepperSmall(180, 5, 8, 2, 10); //SmallHandle
Stepper myStepperSmall(180, 8, 5, 10, 2); //SmallHandle


void setup() {
  Serial.begin(115200);
  Serial.println("ARDUINO LAUNCHING");
  // put your setup code here, to run once:
   myStepperBig.setSpeed(200);
   myStepperSmall.setSpeed(200);

}

void loop() {
  /*myStepperBig.step(720); //2 step per deg
   Serial.println("Waiting");
   delay(500);*/
  
   myStepperBig.step(720);
   Serial.println("Waiting smoll");
   delay(500);
   myStepperBig.step(-720);

}
