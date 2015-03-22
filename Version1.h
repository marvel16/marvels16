#pragma once

#include <iostream>
#include <string>
#include <list>
#include <fstream>

using namespace std;

#define SAVE(ClassName) void RegisterMembers(Members& members) {
#define REG_MEMBER(m) RegisterMember(members, &m);
#define ENDSAVE }

class IMember
{
public:
	virtual void Load(ifstream& file) = 0;
	virtual void Save(ofstream& file) = 0;
	virtual ~IMember() {}
};

//saving member pointers here to serialize\deserialize their values
template<typename T>
class MemberPointer : public IMember
{
public:
	MemberPointer(T* pMember) : p(pMember) {}

	void Save(ofstream& file)
	{
		file << *p << " ";
	}

	void Load(ifstream& file)
	{
		file >> *p;
	}

private:
	T* p;
};

// help class to save member pointers in some container
class Members : public std::list<IMember*>
{
public:
	~Members()
	{
		while (!empty()) delete front(), pop_front();
	}
};

class BaseClass : public IMember{
public:
	void Save(std::ofstream& file)
	{
		Members members;
		RegisterMembers(members);
		for (Members::iterator i = members.begin(); i != members.end(); i++)
			(*i)->Save(file);
	}

	void Load(std::ifstream& file) {
		Members members;
		RegisterMembers(members);
		for (Members::iterator i = members.begin(); i != members.end(); ++i)
			(*i)->Load(file);
	}

	void Save(char* fileName)
	{
		ofstream file(fileName);
		Save(file);
	}

	void Load(char* fileName)
	{
		ifstream file(fileName);
		Load(file);
	}

protected:
	template<typename T>
	void RegisterMember(Members& members, T* pM)
	{
		members.push_back(new MemberPointer<T>(pM));
	}

	virtual void RegisterMembers(Members& members) = 0;
};


// test class Point
class Point : public BaseClass
{
public:
	int x;
	double y;
	char z;
	std::string s;
	SAVE(Point)
		REG_MEMBER(x);
	REG_MEMBER(y);
	REG_MEMBER(z);
	REG_MEMBER(s);
	ENDSAVE;
};


// partitial template instance for overloading >> << operator for Point class
template<>
class MemberPointer<Point> : public IMember
{
public:
	MemberPointer(Point* pMember) : p(pMember) {}
	void Save(ofstream& file)
	{
		p->Save(file);
	}
	void Load(ifstream& file)
	{
		p->Load(file);
	}
private:
	Point* p;
};

class Line : public BaseClass
{
public:
	Point a;
	Point b;
	SAVE(Line)
		REG_MEMBER(a);
	REG_MEMBER(b);
	ENDSAVE;
};