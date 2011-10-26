propassgen - a pronouceable password generator
==============================================

Generate passwords that are easy to pronounce (and thus, to memorize).

The underlying mathematical tool used is a [second order Markov source][1],
meaning that the probability of ocurrence of a character depends on the two
preceding characters in the string.

This is working fairly well in practice, at least for the English language.

__Many thanks to [Tom Van Vleck][2], who came up with this simple and practical
algorithm.__

References & credits:
---------------------
-  Van Vleck, T., [gpw][3]
-  Gasser, M., [A Random Word Generator for Pronounceable Passwords][4]
-  [FIPS PUB 181][5]

[1]: http://www.data-compression.com/theory.shtml#model
[2]: http://www.multicians.org/thvv
[3]: http://www.multicians.org/thvv/gpw.html
[4]: http://www.dtic.mil/cgi-bin/GetTRDoc?AD=ADA017676&Location=U2&doc=GetTRDoc.pdf
[5]: http://www.itl.nist.gov/fipspubs/fip181.htm
