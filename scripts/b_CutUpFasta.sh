#!/bin/bash

fasta=$1
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