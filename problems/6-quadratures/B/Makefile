all: OutB.txt
	cat OutB.txt

OutB.txt: mainB.exe
	mono $< >$@

mainB.exe: mainB.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=$(LIBDIR)/integration/quado8.cs
SRC +=../integrator.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* out* *.exe *.dll *.svg *.txt
