#!/bin/bash
##Usage -t 1-<number of samples> -v folder=<path to folder of paired read fastq files>,primers=<path to primer file>,indexes=<path to indexes>

#############################################
###Job submission config#####################
#############################################
# current environment and current working directory
#$ -V -cwd
# request time
#$ -l h_rt=03:00:00
#request memory per core
#$ -l h_vmem=2G
#request cores
#$ -pe smp 1
################################################
####Job Details#################################
################################################
#Check the data folder exists
echo "Folder of files:"
if [ -d "${folder}" ] ; then
    echo "Good $folder is OK";
elif [ -f "${folder}" ]; then
    echo "Error ${folder} is a file";
    exit 1
else
    echo "Error ${folder} is not valid";
    exit 1
fi

#Load the appropriate boost 
module load boost/1.67.0

#get a read one files from list of files in the data folder, based on the scripts postion in the qsub array ($SGE_TASK_ID)
fastqFile=$(ls $folder/*R1_001.fastq.gz | sed -n -e "$SGE_TASK_ID p")
	echo "File:" $fastqFile 

#Get the R2 files
read1=$fastqFile
read2=$(echo $read1 | sed 's/R1/R2/g')

echo Read1 $read1
echo Read2 $read2	
	
#Make results folder
output=$folder"/Deconvoluted"
mkdir -p $output
echo "Folder: "$output

#Run program
/home/home01/msjimc/deconvolute/Deconvolute.exe $read1 $read2 $primers $indexes $output

#demonstrate script completed and wasn't killed by queuing system
echo done 