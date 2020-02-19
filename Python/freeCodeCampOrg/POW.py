""" POW to show For loops"""

def raise_to_power(base_num,pow_num):
    result = 1
    for index in range(pow_num):
        result = result * base_num
    return result


baseNumber = int(input("Please enter base number: "))
powNumber = int(input("Please enter pow number: "))
print("\n Result of " + str(baseNumber) + "^" + str(powNumber) + " is equal to : " + str(raise_to_power(baseNumber,powNumber)))
print("\n Validation : " + str(pow(baseNumber,powNumber)))

