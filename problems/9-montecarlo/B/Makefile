all: plotB.svg

plotB.svg: plotB.gpi out.B.txt
	gnuplot plotB.gpi


out.B.txt: mainB.exe
	mono $< >$@

mainB.exe: mainB.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../montecarlo.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg 
