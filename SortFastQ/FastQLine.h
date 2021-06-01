#pragma once

#include <vector>
#include <string>
#include <iostream>

using namespace std;

class FastQLine
{
public:
	FastQLine() { lineData.push_back("X"); }
	FastQLine(string name, string Sequence, string Quality);
	//FastQLine(string name, string Sequence, string Quality, string name2, string Sequence2, string Quality2);
	FastQLine(vector<string> data);
	
	void Update(string name, string Sequence, string Quality);
	//void Update(string name, string Sequence, string Quality, string name2, string Sequence2, string Quality2);
	void Update(vector<string> data);

	bool HasData() { return lineData.size() > 2; }
	string Name() const { return lineData[0]; }
	string Sequence() { return lineData[1]; }
	string Quality() { return lineData[2]; }

	string TextToSave(string type);
	//vector<string> TextToSave();

private:
	vector<string> lineData;

};
bool operator<(const FastQLine &fl1, const FastQLine &fl2);
