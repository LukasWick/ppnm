all: out.B.txt plotB.svg
	cat out.B.txt
plotB.svg: plotB.gpi out.fitfun.txt
	gnuplot plotB.gpi


out.B.txt out.fitfun.txt: mainB.exe
	mono $< >$@

mainB.exe: mainB.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../qnewton.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg 
