#!/bin/bash
##usage -t 1-n were n = number of samples
## -v folder=<folder of fasta files>,blastdb=<folder of blastn>
##Fasta files must have sequence data without line breaks.  
#############################################
###Job submission config#####################
#############################################

# current environment and current working directory
#$ -V -cwd
# request time
#$ -l h_rt=48:00:00
#request memory per core
#$ -l h_vmem=15G
#request cores
#$ -pe smp 5

################################################
####Job Details#################################
################################################
##Get the *.fa file
file=$(ls $folder/*.fa | sed -n -e "$SGE_TASK_ID p")
	echo fasta file: $file

echo Folder: $folder
echo Database: $blastdb

saveTool=$(echo $file | sed 's/.fa/_fa/g')
saveToo=$saveTool"_DNA_10_hits/"
	echo Output folder: $saveToo
mkdir -p $saveToo

space=" "
fileNumber=0
while IFS= read -r name
do
	IFS= read -r sequence
 
	let fileNumber=$fileNumber+1
	#newName=${name/$space/-}
	#fileName=${newName:1}
	fileName=$fileNumber
	
	echo $name > $saveToo$fileName.txt
	echo $sequence >> $saveToo$fileName.txt

	COUNTER=0
    while [  $COUNTER -lt 49 ]; do
		read -r name
        IFS= read -r sequence
		echo $name >> $saveToo$fileName.txt
		echo $sequence >> $saveToo$fileName.txt		
        let COUNTER=COUNTER+1 
    done

	echo $saveToo$fileName.xml
	
	if [ -f "$saveToo/$fileName.xml" ] ; then
		echo "processed"
	else
		printf "doing"
		/nobackup/NGS/blast/ncbi-blast-2.11.0+/bin/blastn -query $saveToo$fileName.txt -db $blastdb -dust no -outfmt 5 -num_alignments 10 -num_threads 5 > $saveToo$fileName.xml
		echo " done"
	fi
	rm $saveToo$fileName.txt

done <"$file"


echo done
