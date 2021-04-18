#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
uint16_t value=1;
uint16_t dupa=0;
/* WiFi network name and password */
// Your wirelless router ssid and password
const char * ssid = "Kraina Grzybów"; 
const char * pwd = "alakazam";

// IP address to send UDP data to.
// it can be ip address of the server or 
// a network broadcast address
// here is broadcast address
const char * udpAddress = "192.168.137.1"; // your pc ip
const int udpPort = 8080; //port server

//create UDP instance
WiFiUDP udp;

void setup(){
  Serial.begin(115200); // to monitor activity
  
  //Connect to the WiFi network
   WiFi.begin(ssid, pwd);
  Serial.println("");

  // Wait for connection
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to ");
  Serial.println(ssid);
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());
  //This initializes udp and transfer buffer
  udp.begin(udpPort);
}

void loop(){
  //data will be sent to server
 uint16_t a=analogRead(A0);
 String stringOne =  String(a, DEC);
 
//Serial.println(a);
//Serial.println(a);
Serial.println(stringOne);

int l=stringOne.length();
//Serial.println(l);

//int numChars=sizeof(tab);
//Serial.println(numChars);
    uint8_t buffer[50];//={value,(value>>8)} ;
  for (int i = 0; i < l; i++)
{
  buffer[i]=stringOne[i];
 // Serial.println(buffer[i]);

}

//Serial.println();
  //send hello world to server
  udp.beginPacket(udpAddress, udpPort);
  udp.write(buffer, l);
  udp.endPacket();
  memset(buffer, 0, 50);
  //processing incoming packet, must be called before reading the buffer
  udp.parsePacket();
  //receive response from server, it will be HELLO WORLD
  if(udp.read(buffer, 50) > 0){
    Serial.print("Server to client: ");
    Serial.println((char *)buffer);
  }
  //Wait for 1 second
  value++;
  
  
  delay(1000);
}
