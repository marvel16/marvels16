#include <iostream>
#include <list>
#include <fstream>

#include <string>

using namespace std;

class IMember {
public:
	virtual void Load(ifstream& file) = 0;
	virtual void Save(ofstream& file) = 0;
};

// saving member pointers here to serialize\deserialize their values
class Members : public list<IMember*> {
public:
	~Members() {
		while (!empty()) delete front(), pop_front();
	}
};

// help class to save member pointers in some container and input\output member states to file
template<typename T>
class MemberPointer : public IMember {
public:
	MemberPointer(T* pMember) : p(pMember) {
	}
	void Load(ifstream& file) {
		file >> *p;
	}
	void Save(ofstream& file) {
		file << *p << " ";
	}
private:
	T* p;
};

// class you that u need to inherit to ser\deser members
class BaseClass : public IMember{
public:
	void Load(char* fileName) {
		ifstream file(fileName);
		Load(file);
	}
	void Save(char* fileName) {
		ofstream file(fileName);
		Save(file);
	}
	void Load(ifstream& file) {
		Members members;
		RegisterMembers(members); // create and fill list of members
		for (Members::iterator i = members.begin(); i != members.end(); i++) {
			(*i)->Load(file);
		}
	}
	void Save(ofstream& file) {
		Members members;
		RegisterMembers(members);
		for (Members::iterator i = members.begin(); i != members.end(); i++) {
			(*i)->Save(file);
		}
	}
protected:
	//template<class T>
	//void RegisterMember(Members& members, T* pM) {
	//	members.push_back(new MemberPointer<T>(pM));
	//}
	//template<class T>
	//void RegisterBaseClass(Members& members, T* pM) {
	//	members.push_back(new BaseClassPointer<T>(pM));
	//}
	template<template<typename> class TypeMemberPointer, typename T>
	void RegisterMember(Members& members, T* pM) {
		members.push_back(new TypeMemberPointer<T>(pM));
	}
	virtual void RegisterMembers(Members& members) = 0;
};


// partitial instance of template for overloading >> <<operators in derived class. In example Point - base , Line - derived
template<typename T>
class BaseClassPointer : public IMember {
public:
	BaseClassPointer(T* pMember) : p(pMember) {
	}
	void Load(ifstream& file) {
		p->Load(file);
	}
	void Save(ofstream& file) {
		p->Save(file);
	}
private:
	T* p;
};

#define SAVE(ClassName) void RegisterMembers(Members& members) {
//#define REG_MEMBER(m) RegisterMember(members, &m);
//#define REG_BASECLASS(m) RegisterBaseClass(members, &m);
#define REG_MEMBER(m) RegisterMember<MemberPointer>(members, &m);
#define REG_BASECLASS(m) RegisterMember<BaseClassPointer>(members, &m);
#define ENDSAVE }

class Point : public BaseClass {
public:
	int x;
	double y;
	char z;
	string s;
	SAVE(Point)
	REG_MEMBER(x);
	REG_MEMBER(y);
	REG_MEMBER(z);
	REG_MEMBER(s);
	ENDSAVE;
};

class Line : public BaseClass {
public:
	Point a;
	Point b;
	SAVE(Line)
	REG_BASECLASS(a);
	REG_BASECLASS(b);
	ENDSAVE;
};

