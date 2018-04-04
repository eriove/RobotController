#include "passwords.h"
#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>
#include <Servo.h> 

const char *ssid = "EoE";
// password in passwords.h

ESP8266WebServer server ( 80 );

const int num_servos = 5;
Servo servos[num_servos];

const int leftMotorPin = 4;
const int rightMotorPin = 5;
const int headPin = 2;
const int eyePin1 = 15;
const int eyePin2 = 13;

void handleServos() {
	int numArgs = server.args();
	for (int i = 0; i < numArgs && i < num_servos; i++) {
		int servo = server.argName(i).toInt();
		if (servo < num_servos) {
			int value = server.arg(i).toInt();
			servos[servo].write(value);
			Serial.print("Wrote ");
			Serial.print(value);
			Serial.print(" to servo ");
			Serial.println(servo);
		}
		else {
			Serial.println("Servo number too large");
		}
	}
	handleAnalog();
}


void handleAnalog() {
  float v = analogRead(A0)*0.01141258741258741258741258741259;
  Serial.println(v);
  char cstr[16];
  itoa(v*100,cstr,10);
  server.send(200, "text/plain", cstr);
}

void handleRoot() {
	char temp[400];
	int sec = millis() / 1000;
	int min = sec / 60;
	int hr = min / 60;

	snprintf ( temp, 400,

"<html>\
  <head>\
    <meta http-equiv='refresh' content='5'/>\
    <title>ESP8266 Demo</title>\
    <style>\
      body { background-color: #cccccc; font-family: Arial, Helvetica, Sans-Serif; Color: #000088; }\
    </style>\
  </head>\
  <body>\
    <h1>Hello from ESP8266!</h1>\
    <p>Uptime: %02d:%02d:%02d</p>\
  </body>\
</html>",

		hr, min % 60, sec % 60
	);
	server.send ( 200, "text/html", temp );
}

void handleNotFound() {
	String message = "File Not Found\n\n";
	message += "URI: ";
	message += server.uri();
	message += "\nMethod: ";
	message += ( server.method() == HTTP_GET ) ? "GET" : "POST";
	message += "\nArguments: ";
	message += server.args();
	message += "\n";

	for ( uint8_t i = 0; i < server.args(); i++ ) {
		message += " " + server.argName ( i ) + ": " + server.arg ( i ) + "\n";
	}

	server.send ( 404, "text/plain", message );
}

void setup ( void ) {
	Serial.begin ( 115200 );
	WiFi.begin ( ssid, password );
	Serial.println ( "" );

	// Wait for connection
	while ( WiFi.status() != WL_CONNECTED ) {
		delay ( 500 );
		Serial.print ( "." );
	}

	Serial.println ( "" );
	Serial.print ( "Connected to " );
	Serial.println ( ssid );
	Serial.print ( "IP address: " );
	Serial.println ( WiFi.localIP() );

	if ( MDNS.begin ( "walle" ) ) {
		MDNS.addService("http", "tcp", 80);
		Serial.println ( "MDNS responder started" );
	}
	

	server.on ( "/", handleRoot );
	server.on ( "/analog", handleAnalog );
	server.on("/servos", handleServos);
	server.on ( "/inline", []() {
		server.send ( 200, "text/plain", "this works as well" );
	} );
	server.onNotFound ( handleNotFound );
	server.begin();
	Serial.println ( "HTTP server started" );

	servos[0].attach(4);   
	servos[1].attach(5);  
	servos[2].attach(2);  
	servos[3].attach(16);
	servos[4].attach(0);
	pinMode(eyePin1, OUTPUT);
	pinMode(eyePin2, OUTPUT);

	servos[3].write(128);
	servos[4].write(128);

	analogWrite(eyePin1, 255);
	analogWrite(eyePin2, 128);
}

void loop ( void ) {
	server.handleClient();
	//delay(10);
	//MDNS.update();
}

