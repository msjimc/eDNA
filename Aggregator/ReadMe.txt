Build from source code
The source code can be compiled by the g++ compiler on a Linux system on which g++ and the Boost libraries have been installed. 
Open a terminal and navigate to the folder containing the source code and build the application using the command line below. 
Any error messages will be saved to a file called errors.txt. If the build is successful the errors.txt will be empty and the 
application file 'Aggregator.exe' will appear in the folder.

g++ -g Aggregator.cpp DataFromFasta.cpp  -I <path to boost folder> -L <path to boost folder>/lib -l boost_iostreams -o Aggregator.exe 2> errors.txt


Run the application via qsub
The script q_Aggregator_eDNA.sh is an example of a script to run the program on a HPC cluster operating the SGE/qsub queuing system.
The line:
module load boost/1.67.0
loads the relevant boost library in to the environment and may need modifing for your HPC system.

Run the appliction via the terminal
Navigate to the folder containing the application and issue the command below:
Aggregator.exe $folder $size
Where:
$folder is the path to the folder of fasta files exported by SortFastQ that will be included in the analysis
$size is the typical size of the expected amplicon