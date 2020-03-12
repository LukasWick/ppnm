all: out.txt Energy.svg

Energy.svg: energy.gpi outFile.txt 
	gnuplot $<

outFile.txt: psiMain.exe
	mono $< > $@

out.txt: main.exe
	mono $< > $@
main.exe: main.cs matlib.dll
	mcs $< -reference:matlib.dll -out:$@

psiMain.exe: psiMain.cs matlib.dll
	mcs $< -reference:matlib.dll -out:$@

LIBDIR = ../../matlib
SRC  = $(LIBDIR)/integration/quado8.cs
SRC += $(LIBDIR)/integration/quado4.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@
clean: 
	rm -f *.exe out*
