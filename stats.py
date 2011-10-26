#!/usr/bin/env python
import sys
import string
import operator

def main():
    word_freq = dict()
    trig_freq = dict()
    trig_vers = dict()

    # Get word frequencies.
    for line in sys.stdin:
        for word in line.split():
            # Sanitize the word.
            word = word.translate(None, string.punctuation).upper()
            if is_valid(word):
                word_freq[word] = word_freq.get(word, 0) + 1

    # Get trigram frequencies / versatilities.
    for word, freq in word_freq.iteritems():
        if len(word) >= 3:
            for i in range(len(word) - 2):
                trig_freq[word[i:i+3]] = trig_freq.get(word[i:i+3], 0) + freq
                trig_vers[word[i:i+3]] = trig_vers.get(word[i:i+3], 0) + 1

    pretty_print(trig_vers)
    print len(trig_freq)

def is_valid(word):
    for i in range(len(word)):
        if word[i] not in string.ascii_uppercase:
            return False
    return True

def pretty_print(table):
    table = sorted(table.iteritems(), key=operator.itemgetter(1), reverse=True)
    for key, value in table:
        print "%s: %s" % (key, value)

if __name__ == '__main__':
    main()
