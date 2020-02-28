all: plotA.svg plotB1.svg plotB2.svg plotB3.svg

plotA.svg: plotA.gpi outA.txt
	gnuplot $<

plotB1.svg: plotB1.gpi outB1.txt
	gnuplot $<

plotB2.svg: plotB2.gpi outB2.txt
	gnuplot $<

plotB3.svg: plotB3.gpi outB3.txt
	gnuplot $<

outA.txt: mainA.exe
	mono $< 1> $@ 2>logA

outB1.txt: mainB.exe
	mono $<  1> $@ 2>logB1

outB2.txt: mainB.exe
	mono $< udiff0=0.5 1> $@ 2>logB2

outB3.txt: mainB.exe
	mono $< udiff0=0.5 eps=0.01 N=10 1> $@ 2>logB3


mainA.exe: mainA.cs matlib.dll
	mcs -reference:matlib.dll $<

mainB.exe: mainB.cs matlib.dll
	mcs -reference:matlib.dll $<


LIBDIR = ../matlib
matlib.dll: $(LIBDIR)/matrix/vector.cs $(LIBDIR)/ode/ode.cs 
	mcs $^ -target:library -out:$@

clean: 
	rm -f *.exe *.dll out.* *.svg