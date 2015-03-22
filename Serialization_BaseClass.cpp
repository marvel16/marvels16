#include "stdafx.h"

#include "serialization.h"


int main() {
	Line l;
	Point& p = l.a; // point member reference for easier usage
	
	p.x = 1;
	p.y = 2.0;
	p.z = 'a';
	p.s = "Hello World!";
	

	l.b = l.a;
	l.Save("point.txt");
	
	// create new line and load state from file
	Line l2;
	
	l2.Load("point.txt");
	cout << "p.x = " << p.x << endl;
	cout << "p.y = " << p.y << endl;
	cout << "p.z = " << p.z << endl;
	cout << "p.s = " << p.s << endl;
	return 0;
}
