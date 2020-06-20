all: OutC.txt
	cat OutC.txt

OutC.txt: mainC.exe
	mono $< >$@

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=simplex.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* Out*  *.exe *.dll *.svg *.txt
