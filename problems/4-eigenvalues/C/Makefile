all: plotC.svg out.C.txt 
	cat out.C.txt

plotC.svg: plotC.gpi out.plotC.txt
	gnuplot $<

out.C.txt out.plotC.txt: mainC.exe
	mono $< > out.C.txt

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs

matlib.dll: ../jac.diag.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
