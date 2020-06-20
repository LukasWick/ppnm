all: OutC.txt
	cat  $<

OutC.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs

matlib.dll: gr.Givens.cs $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.txt *.exe *.dll
