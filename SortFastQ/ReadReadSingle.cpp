#include "ReadReadSingle.h"

ReadReadSingle::ReadReadSingle(string FileName)
{
	fileName = FileName;

	file.exceptions(std::ios::failbit | std::ios::badbit);
	file.open(fileName.c_str(), std::ios_base::in | std::ios_base::binary);

	decompressor.push(boost::iostreams::gzip_decompressor());
	decompressor.push(file);	
}

ReadReadSingle::~ReadReadSingle()
{
	try { if (!file) { file.close(); } }
	catch (exception ex) {}
}

void ReadReadSingle::SetFileInformation(string FileName)
{
	fileName = FileName;

	file.exceptions(std::ios::failbit | std::ios::badbit);
	file.open(fileName.c_str(), std::ios_base::in | std::ios_base::binary);

	decompressor.push(boost::iostreams::gzip_decompressor());
	decompressor.push(file);
	GetNextRead();

}

void ReadReadSingle::CloseFile()
{
	file.close();
}

void ReadReadSingle::GetNextRead()
{

	vector<string> data;
	string line;
	int tracker = 0;

	for (int index = 0; index < 4; index++)
	{
		if (getline(decompressor, line)) { tracker++; }
		data.push_back(line);
	}	

	if (tracker != 4)
	{		
		data[0] = "X";
		data[1] = "X";
		data[2] = "X";
		data[3] = "X";
		Read.Update(data);
	}
	else
	{ Read.Update(data); }
	
}

FastQLine ReadReadSingle::GetRead() 
{ return Read; }

bool operator<(const ReadReadSingle &RRS1, const ReadReadSingle &RRS2)
{
	return RRS1.Name().compare(RRS2.Name()) < 0;
}
