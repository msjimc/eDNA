// Aggregator.cpp : Defines the entry point for the console application.
//
#include <map>
#include <vector>
#include <string>
#include <iostream>
#include <fstream>
#include <dirent.h>
#include "DataFromFasta.h"
#include <string>

using namespace std;

vector<string> getListOfFastaFiles(string folder, bool withPath)
{
	vector<string> listOfFastaFiles;

	DIR *dir;
	struct dirent *ent;
	std::string fileNameFasta;
	std::string slash = "/";
	size_t size = folder.find_last_of("/");
	size_t len = folder.length();
	if (size == len - 1)
	{
		slash = "";
	}

	string fileName;

	if ((dir = opendir(folder.c_str())) != NULL) {
		/* print all the files and directories within directory */
		while ((ent = readdir(dir)) != NULL)
		{
			fileNameFasta = ent->d_name;
			int index = (int)fileNameFasta.rfind(".fasta.gz");
			if (ent->d_type == DT_REG && index == fileNameFasta.length() - 9)
			{
				if (withPath == true)
				{
					listOfFastaFiles.push_back(folder + slash + fileNameFasta);
				}
				else
				{
					listOfFastaFiles.push_back(fileNameFasta);
				}
			}
		}
		closedir(dir);
	}
	else
	{
		/* could not open directory */
		listOfFastaFiles.clear();
		return listOfFastaFiles;
	}

	return listOfFastaFiles;
}

int main(int argc, char *argv[])
{
	if (argc != 3)
	{
		cout << "Wrong parameter number\n";
		
		for (int index = 0; index < argc; index++)
		{ cout << argv[index] << "\n"; }
	
		cout << "\nShould be: Folder (with path) containing fasta files, Approximate size of amplicon.\n";
		return 1;
	}
	
	int productSize = atoi(argv[2]);
	
	string folder(argv[1]);	
	int numberOfFastaFiles = 0;	
	map<string, map<int, int> > fastaCounts;
	vector<string> listOfFastaFiles;
	try
	{
		listOfFastaFiles = getListOfFastaFiles(folder, true);
		numberOfFastaFiles = listOfFastaFiles.size();		

		for (int index = 0; index < numberOfFastaFiles; index++)
		{
			cout << index << "\t" << listOfFastaFiles[index] << "\n";

			DataFromFasta *DFF = new DataFromFasta(listOfFastaFiles[index], productSize);

			fastaCounts = DFF->processData(fastaCounts, index);

			delete DFF;
		}
	}
	catch (exception ex)
	{
		cout << ex.what() << "\n";
	}
	
	
	ofstream file;
	file.exceptions(ofstream::failbit | ofstream::badbit);
	ofstream fileFasta;
	fileFasta.exceptions(ofstream::failbit | ofstream::badbit);
	try
	{
		file.open((folder + "/output.txt").c_str());
		
		fileFasta.open((folder + "/sequence.fa").c_str());
		int counter = 0;
		
		for (int index = 0; index < numberOfFastaFiles; index++)
		{
			file << listOfFastaFiles[index] << "\t";

		}
		file << "Sequence\n";

		map<int, int>::iterator it;
		map<string, map<int, int> >::iterator vF;
		for (vF = fastaCounts.begin(); vF != fastaCounts.end(); vF++)
		{
			counter++;
			file << counter << "\t";
			fileFasta << ">" << counter << "\n" + vF->first + "\n";

			for (int index = 0; index < numberOfFastaFiles; index++)
			{
				it = vF->second.find(index);
				if (it != vF->second.end())
				{
					file << it->second << "\t";
				}
				else
				{
					file << "0\t";
				}
			}	
			file << vF->first << "\n";			
		}
	}
	catch (exception ex)
	{
		cout << ex.what() << "\n";
		
	}

	if (!file) { file.close(); }
	if (!fileFasta) { fileFasta.close(); }

    return 0;
}

