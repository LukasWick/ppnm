all: OutA1.txt plotA.psi.svg plotA.E.svg
	cat OutA1.txt

plotA.psi.svg: plotA.psi.gpi out.plotA.psi.txt
	gnuplot $<

plotA.E.svg: plotA.E.gpi out.plotA.E.txt
	gnuplot $<

OutA1.txt out.plotA.E.txt out.plotA.psi.txt: mainA.exe
	mono $< > $@

mainA.exe: mainA.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs


matlib.dll: ../jac.diag.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
