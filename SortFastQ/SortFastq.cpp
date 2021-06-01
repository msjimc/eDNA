// SortFastq is the entry point of the program

#include <string>
#include "Engine.h"

int main(int argc, char *argv[])
{
	if (argc == 4)
	{
		string Filename(argv[1]);
		string folder(argv[2]);
		string type(argv[3]);
		
		cout << Filename << "\n" << folder << "\n" << type << "\n";
		
		Engine eng(Filename, folder, type);
	}
	else
	{cout << "Wrong number of arguments filename of target file, folder path to save data too and eiter A for fasta or Q for fastq format\n";}
	
	return 0;
}