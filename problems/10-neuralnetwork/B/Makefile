all: PlotB.svg


PlotB.svg: plotB.gpi out.tabfun.sin.txt out.fitfun.sin.txt out.exactfun.sin.txt
	gnuplot plotB.gpi

out.tabfun.sin.txt out.fitfun.sin.txt out.exactfun.sin.txt: mainB.exe
	mono $< 

mainB.exe: mainB.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../../8-multimin/qnewton.cs
SRC +=../interpolater.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
