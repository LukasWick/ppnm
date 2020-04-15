all: out.C.txt
	cat out.C.txt

out.C.txt: mainC.exe
	mono $< >$@

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/integration/quado8.cs
SRC +=../integrator.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
