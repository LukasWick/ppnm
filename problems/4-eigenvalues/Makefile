all: outA.txt plotA.svg plotB1.svg
	cat outA.txt

plotA.svg: plotA.gpi out.plotA.txt
	gnuplot $<

plotB1.svg: plotB1.gpi out.plotB1.txt
	gnuplot $<

outA.txt out.plotA.txt out.plotB1.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs


matlib.dll: jac.diag.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.txt *.exe *.dll *.svg
