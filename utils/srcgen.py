#!/usr/bin/env python

# Copyright (C) 2011 Lucas Maystre <lucas@maystre.ch>
#
# This file is part of propassgen.
#
# propassgen is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# propassgen is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with propassgen.  If not, see <http://www.gnu.org/licenses/>

import time
import string
import operator
import optparse

from xml.etree import ElementTree


def _init_trigram_table(symbols):
    table = {}
    for x in symbols:
        table[x] = {}
        for y in symbols:
            table[x][y] = {}
            for z in symbols:
                table[x][y][z] = 0
    return table


def word_freq(symbols, files):
    freq = dict()
    for f in files:
        for line in open(f, 'r'):
            for word in line.split():
                # Sanitize the word.
                word = word.translate(None, string.punctuation).lower()
                if _is_valid(symbols, word):
                    freq[word] = freq.get(word, 0) + 1
    return freq


def trigram_freq(symbols, files, is_vers=False):
    w_freq = word_freq(symbols, files)
    trig_freq = _init_trigram_table(symbols)
    # Get trigram frequencies / versatilities.
    for word, freq in w_freq.iteritems():
        if len(word) >= 3:
            for i in range(len(word) - 2):
                x, y, z = tuple(word[i:i+3])
                trig_freq[x][y][z] += 1 if is_vers else freq
    return trig_freq


def _is_valid(symbols, word):
    for i in range(len(word)):
        if word[i] not in symbols:
            return False
    return True


def _generate_comment(files=None):
    # TODO Handle the case where files is None.
    return """
################################
Generated by srcgen.py on %s.

Files used for source modelling:
- %s
################################
""" % (time.strftime('%Y-%m-%d at %H:%M:%S'), "\n- ".join(files))

# Taken from http://effbot.org/zone/element-lib.htm#prettyprint
def _indent(elem, level=0):
    i = "\n" + level*"  "
    if len(elem):
        if not elem.text or not elem.text.strip():
            elem.text = i + "  "
        if not elem.tail or not elem.tail.strip():
            elem.tail = i
        for elem in elem:
            _indent(elem, level+1)
        if not elem.tail or not elem.tail.strip():
            elem.tail = i
    else:
        if level and (not elem.tail or not elem.tail.strip()):
            elem.tail = i

def generate_xml(symbols, frequencies, files=None):
    root_elem = ElementTree.Element('source')
    root_elem.append(ElementTree.Comment(_generate_comment(files)))

    # Create the symbols list.
    symbols_elem = ElementTree.SubElement(root_elem, 'symbols')
    for symbol in symbols:
        attribs = { 'value': symbol }
        ElementTree.SubElement(symbols_elem, 'symbol', attrib=attribs)

    # Create the sequences list.
    seqs_elem = ElementTree.SubElement(root_elem, 'sequences')
    for char1, marginal_freq in frequencies.iteritems():
        for char2, sub_marginal_freq in marginal_freq.iteritems():
            for char3, trig_freq in sub_marginal_freq.iteritems():
                attribs = {
                    'value': char1+char2+char3,
                    'frequency': str(trig_freq),
                }
                ElementTree.SubElement(seqs_elem, 'sequence', attrib=attribs)

    _indent(root_elem)
    return ElementTree.tostring(root_elem, encoding='utf-8')


# def pretty_print(table):
#     items = sorted(table.iteritems(), key=operator.itemgetter(1), reverse=True)
#     for key, value in items:
#         print "%s: %s" % (key, value)


def _parse_command():
    parser = optparse.OptionParser()
    parser.add_option('--versatility', action='store_true',
            dest='is_versatility', default=False)
    parser.add_option('--symbols', action='store', type='string',
            dest='symbols', default=string.ascii_lowercase)
    parser.add_option('--output', action='store', type='string',
            dest='output', default='source.xml')
    return parser.parse_args()


if __name__ == '__main__':
    options, files = _parse_command()
    frequencies = trigram_freq(options.symbols, files, options.is_versatility)
    open(options.output, 'w').write(
            generate_xml(options.symbols, frequencies, files))