transformed=
while IFS= read -r line; do
	IFS=' ' read -r -a array <<< "$line"
	echo "line=$line"
	echo ${array[0]}
	echo ${array[1]}
	if [ -z "$transformed" ]; then
		transformed=("${array[@]}")
		echo --initial--
			echo ${transformed[0]}
			echo ${transformed[1]}
		for index in "${!transformed[@]}"
		do
			echo $index
			echo ${transformed[index]}
		done
		echo --end initial--
	else
		for index in "${!array[@]}"
		do
			echo ${transformed[index]} ' ' ${array[index]}
			transformed[index]="$(echo ${transformed[index]} ' ' ${array[index]})"
		done
	fi
done < file.txt

echo ---------------------

for index in "${!transformed[@]}"
do
	echo ${transformed[index]}
done
