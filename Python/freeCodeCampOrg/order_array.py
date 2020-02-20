""" Order Array by the typical bubble algorithm"""

numbers = [4,2,3,1,45,2,44,5,23,678,435]
orderedNubers = []
print("Before ordering")
print(numbers)
numbersLen = len(numbers)
swapped = True
while swapped:
    swapped = False
    for i in range(numbersLen):
        if ( ( i+1 < numbersLen)  and (numbers[i]  > numbers[i+1])):
            print(str(numbers[i]) + " > " + str(numbers[i+1]))
            """ SWAP Items """
            numbers[i], numbers[i+1] = numbers[i+1], numbers[i] 
            swapped = True

            

            
print("After ordering ")
print(numbers)