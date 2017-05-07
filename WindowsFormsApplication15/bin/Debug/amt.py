import sys
import wikipedia
z = "abcdefghigklmnobqrstuvwxyz"
s = wikipedia.summary("Microsoft", sentences = 1)
s = s.split()
w = ""
for i in s :
    for j in i :
        if j.lower() in z:
                            w += j
    w += " "
print w