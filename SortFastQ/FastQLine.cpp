#include "FastQLine.h"

FastQLine::FastQLine(string name, string Sequence, string Quality)
{
	Update(name, Sequence, Quality);
}

//FastQLine::FastQLine(string name, string Sequence, string Quality, string name2, string Sequence2, string Quality2)
//{
//	Update(name, Sequence, Quality, name2, Sequence2, Quality2);
//}

FastQLine::FastQLine(vector<string> data)
{
	Update(data);
}

void FastQLine::Update(string name, string Sequence, string Quality)
{
	lineData.clear();
	lineData.push_back(name.substr(0));
	lineData.push_back(Sequence);
	lineData.push_back(Quality);
}

//void FastQLine::Update(string name, string Sequence, string Quality, string name2, string Sequence2, string Quality2)
//{
//	lineData.clear();
//	lineData.push_back(name);
//	lineData.push_back(Sequence);
//	lineData.push_back(Quality);
//	lineData.push_back(name2);
//	lineData.push_back(Sequence2);
//	lineData.push_back(Quality2);
//}

void FastQLine::Update(vector<string> data)
{
	lineData.clear();
	switch (data.size())
	{
	case 3:
		lineData.push_back(data[0]);
		lineData.push_back(data[1]);
		lineData.push_back(data[2]);
		break;
	case 4:
		lineData.push_back(data[0]);
		lineData.push_back(data[1]);
		lineData.push_back(data[3]);
		break;
	/*case 6:
		lineData.push_back(data[0]);
		lineData.push_back(data[1]);
		lineData.push_back(data[2]);
		lineData.push_back(data[3]);
		lineData.push_back(data[4]);
		lineData.push_back(data[5]);
		break;
	case 8:
		lineData.push_back(data[0]);
		lineData.push_back(data[1]);
		lineData.push_back(data[3]);
		lineData.push_back(data[4]);
		lineData.push_back(data[5]);
		lineData.push_back(data[7]);
		break;*/
	}
}

bool operator<(const FastQLine &fl1, const FastQLine &fl2)
{
	return fl1.Name().compare(fl2.Name()) < 0;
}

//vector<string> FastQLine::TextToSave()
//{
//	string textLine = "";
//	textLine = lineData[0] + "\n";
//	textLine += lineData[1] + "\n";
//	textLine += "+\n";
//	textLine += lineData[2] + "\n";
//	vector<string> answer;
//	answer.push_back(textLine);
//
//	if (lineData.size() == 6)
//	{
//		textLine = lineData[3] + "\n";
//		textLine += lineData[4] + "\n";
//		textLine += "+\n";
//		textLine += lineData[5] + "\n";
//		answer.push_back(textLine);
//	}
//
//	return answer;
//}

string FastQLine::TextToSave(string type)
{
	string textLine = "";
	if (type.compare("Q") == 0)
	{
		textLine = "@" + lineData[0] + "\n";
		textLine += lineData[1] + "\n";
		textLine += "+\n";
		textLine += lineData[2] + "\n";
	}
	else if (type.compare("A") == 0)
	{
		textLine = ">" + lineData[0] + "\n";
		textLine += lineData[1] + "\n";
	}
	else
	{
		textLine = "@" + lineData[0] + "\n";
		textLine += lineData[1] + "\n";
		textLine += "+\n";
		textLine += lineData[2] + "\n";
	}
	return textLine;
}