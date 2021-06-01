Build from source code
The source code can be compiled by the g++ compiler on a Linux system on which g++ and the Boost libraries have been installed. 
Open a terminal and navigate to the folder containing the source code and build the application using the command line below. 
Any error messages will be saved to a file called errors.txt. If the build is successful the errors.txt will be empty and the 
application file 'Deconvolute.exe' will appear in the folder.

g++ Deconvolute.cpp Engine.cpp DataStore.cpp -I <path to boost install folder> -L <path to boost install folder>/lib -lboost_iostreams -o Deconvolute.exe 2> errors.txt

Run the application via qsub
The script qS_Deconvolute_twin_index.sh is an example of a script to run the program on a HPC cluster operating the SGE/qsub queuing system, that creates a job array with 
each pair of read 1 nad 2 files processed independantly.
The line:
module load boost/1.67.0
loads the relevant boost library in to the environment and may need modifing for your HPC system.

Run the appliction via the terminal
Navigate to the folder containing the application and issue the command below:
Deconvolute.exe $read1 $read2 $primers $indexes $output

Where:
$read1 is the name with path of the read 1 data file as a gzip compressed fastq file
$read2 is the name with path of the read 2 data file as a gzip compressed fastq file
$primers is the name and path of a text file containing the sequences of the primers used to amplify the amplicons, one sequence per line and the sequence as its synthesised (the 
reverse primer is not reverse complemented). The file 'primers.txt' shows the files format.
$indexes is the name with path of a text file containing the sequences of the indexes used to identify the primers. The files as the same format as the 'primers' file: one sequence 
per line and the indexes in the reverse primers are not reverse complemented. The indexes.txt files shows the format of a index file.
$output is the path to a folder to save the output data in.