all: OutC.txt
	cat OutC.txt

OutC.txt: mainC.exe
	mono $< >$@

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/integration/quado8.cs
SRC +=../integrator.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* out* *.exe *.dll *.svg *.txt
