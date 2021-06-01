#!/bin/bash
##Usage -t 1-n -v folder=<folder to of combined files>

#############################################
###Job submission config#####################
#############################################
# current environment and current working directory
#$ -V -cwd
# request time
#$ -l h_rt=00:10:00
#request memory per core
#$ -l h_vmem=2G
#request cores
#$ -pe smp 1
################################################
####Job Details#################################
################################################
#Check folder exists
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

#load boost libraries
module load boost/1.67.0

#get a *.fastq.gz file from list of files in the data folder, based on the scripts postion in the qsub array ($SGE_TASK_ID)
fastqFile=$(ls $folder/*.fastq.gz | sed -n -e "$SGE_TASK_ID p")
	echo "File:" $fastqFile read1.fastq.gz file

#Make output library
parentdir=`dirname $folder`	
output=$parentdir"/FilteredAndSorted"
mkdir -p $output
echo "Folder: "$output

#Run the progem
SortFastQ.exe $fastqFile $output A

#Check the analysis completed and wasn't killed by the queuing system
echo done 