out.png: plot.complex.gpi out.complex.txt
	gnuplot $<
out.complex.txt: main.exe
	mono $< > $@

main.exe: main.complex.cs math.dll
	mcs $< -reference:math.dll -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/complex/complex.cs
SRC +=$(LIBDIR)/complex/cmath.cs
math.dll: gamma.cs $(SRC)
	mcs $^ -target:library -out:$@ 

.PHONEY:clean
clean:
	rm --force *.dll *.exe out* *.svg

