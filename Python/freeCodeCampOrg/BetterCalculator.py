""" Better Calculator """
print (" _.........._")
print ("| |Python 3| |")
print ("| |        | |")
print ("| |        | |")
print ("| |________| |")
print ("|   ______   |")
print ("|  |    | |  |")
print ("|__|____|_|__|\n\n")

print ("Welcome to Better Calculator !!\n")
num1 = float(input("enter number 1: "))
opt = input("Enter Operation: ")
num2 = float(input("enter number 2: "))

if(opt == "+"):
    print(num1 + num2)
elif(opt == "-"):
    print(num1 - num2)
elif(opt == "*"):
    print(num1 * num2)
elif(opt == "/"):
    print(num1 / num2)
else:
    print("Invalid operator !")




