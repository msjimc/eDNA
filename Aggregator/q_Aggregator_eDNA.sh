#!/bin/bash
##Usage -v folder=<folder of paired read fasta files produced by SortFastQ>,size=<Size of product+/-30 bp>

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
#checks the folder is present
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

#Loads boost libraries in to environment
module load boost/1.67.0

#Sends parameter to output stream 
echo Folder $folder
echo Size $size bp
echo Upper: $((size+30)) bp
echo Lower: $((size-30)) bp
	
#runs the application, the if the system can't find Aggregator, you may need to add the path to the application in the command
Aggregator.exe $folder $size
#indicates the script completed and wasn't kiled by queuing system
echo done 