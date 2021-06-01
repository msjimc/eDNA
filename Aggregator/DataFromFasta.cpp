#include "DataFromFasta.h"

DataFromFasta::~DataFromFasta()
{
	if (!file) { file.close(); }
}

DataFromFasta::DataFromFasta(string FileName, int productSize)
{
	ProductSize = productSize;
	fileName = FileName;
	file.exceptions(std::ios::failbit | std::ios::badbit);
	file.open(fileName.c_str(), std::ios_base::in | std::ios_base::binary);

	decompressor.push(boost::iostreams::gzip_decompressor());
	decompressor.push(file);
}

map<string, map<int, int> > DataFromFasta::processData(map<string, map<int, int> > fileCounts, int fileIndex)
{
	string name;
	string sequence;

	while (getline(decompressor, name))
	{
		if (getline(decompressor, sequence))
		{
			string target = sequence.substr(12, sequence.length() - 24);
			if (target.length() < ProductSize + 30 && target.length() > ProductSize - 30)
			{
				map<string, map<int, int> >::iterator it = fileCounts.find(target);
				if (it == fileCounts.end())
				{ 
					map<int, int> readCount;
					readCount.insert(pair<int, int>(fileIndex, 1));
					fileCounts.insert(pair<string, map<int, int> >(target, readCount));
				}
				else
				{
					map<int, int>::iterator itmap = it->second.find(fileIndex);
					if (itmap == it->second.end())
					{ 
						it->second.insert(pair<int, int>(fileIndex, 1));
					}
					else
					{
						itmap->second++;
					}
				}
			}
		}
	}
	return fileCounts;
}



