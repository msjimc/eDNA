#!/bin/bash
##usage -t 1-n -v folder=<folder of fasta files to cut up>
##Fasta files must have sequence data without line breaks.  
#############################################
###Job submission config#####################
#############################################

# current environment and current working directory
#$ -V -cwd
# request time
#$ -l h_rt=01:00:00
#request memory per core
#$ -l h_vmem=2G
#request cores
#$ -pe smp 1

################################################
####Job Details#################################
################################################
##Get the *.fa file

fasta=$(ls $folder/*.fa | sed -n -e "$SGE_TASK_ID p")
saveTool=$(echo $fasta | sed 's/.fa/_fa_single/g')
saveToo=$saveTool"/"

echo "Fasta file "$fasta
echo "Save too " $saveToo

mkdir -p $saveToo

space=" "
dot="."
fileNumber=0

while IFS= read -r name
do
	IFS= read -r sequence
	let fileNumber=fileNumber+1
	newName=${name/$space/-}	
	#fileName=${newnewName:1:10}
	fileName=$fileNumber
	echo "A file " $saveToo$fileName.fa
	echo $name > $saveToo$fileName.fa
	echo $sequence >> $saveToo$fileName.fa

	COUNTER=0
    while [  $COUNTER -lt 4999 ]; do
		read -r name
        	IFS= read -r sequence
		echo $name >> $saveToo$fileName.fa
		echo $sequence >> $saveToo$fileName.fa		
        let COUNTER=COUNTER+1 
    done
	

done <"$fasta"


