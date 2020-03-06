""" 
GIven an ordered list, insert an element:
From brilliant.org:

    1. Place xxx at the end of AAA.
    2. Consider the element to the left of xxx. There are three possibilities.
        If there is no element to the left, then we are done, since xxx will be the smallest element and it is already at the start of the array.
        If xxx is greater than or equal to it, then we are done; xxx is on the right of all smaller elements and on the left of all larger elements, so the array is once again sorted.
        If xxx is smaller, then it should come before the other element. Switch the two to put xxx in front.
    3. Repeat step two until finished.

"""


def insertIntoList(l,e):
    l.append(e)
    i = len(l) - 1
    while i > 0:
        if(l[i-1] <= e):
            break
        else:
            l[i-1], l[i] = l[i], l[i-1]
            i = i-1
    return l


numbers = [2,3,4,5]
print(numbers)
numbers = insertIntoList(numbers,1)
print(numbers)