""" Dictionaries """

monthConversions = {
    "Jan": "January",
    "Feb": "February",
    "Mar": "March",
}

print(monthConversions["Jan"])
print(monthConversions.get("Feb"))
print(monthConversions.get("Nov", "Not a valid key"))