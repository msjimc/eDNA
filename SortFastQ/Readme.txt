Build from source code
The source code can be compiled by the g++ compiler on a Linux system on which g++ and the Boost libraries have been installed. 
Open a terminal and navigate to the folder containing the source code and build the application using the command line below. 
Any error messages will be saved to a file called errors.txt. If the build is successful the errors.txt will be empty and the 
application file 'SortFastq.exe' will appear in the folder.

g++ SortFastq.cpp ReadReadSingle.cpp FastQLine.cpp Engine.cpp -I<path to boost install folder> -L <path to boost install folder>/lib -lboost_iostreams -o SortFastQ.exe 2> error.txt

Run the application via qsub
The script qS_Filter_Sort_FastQ.sh is an example of a script to run the program on a HPC cluster operating the SGE/qsub queuing system.
The line:
module load boost/1.67.0
loads the relevant boost library in to the environment and may need modifing for your HPC system.

Run the appliction via the terminal
Navigate to the folder containing the application and issue the command below:
qS_Filter_Sort_FastQ.exe $fastqFile $output A
Where:
$fastqFile - the name with path of the fastq file to process
A or Q - the presence of A or Q sets the the format of the exported file Q = fastq and A = fasta.