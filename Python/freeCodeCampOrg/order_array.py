""" Order Array by the typical bubble algorithm"""


def bubbleOrderList(l):
    swp = True
    try:
        while swp:
            swp = False
            for i in range(len(l)):
                if ( ( i+1 < len(l))  and (l[i]  > l[i+1])):
                    l[i], l[i+1] = l[i+1], l[i] 
                    swp = True
    except:
        print("Error. Please try again." + err)
    
    return l




numbers = [4,2,3,1,45,2,44,5,23,678,435]
print("Before ordering")
print(numbers)

bubbleOrderList(numbers)

print("After ordering ")
print(numbers)