all: PlotA.svg


PlotA.svg: plotA.gpi out.tabfun.sin.txt out.fitfun.sin.txt
	gnuplot plotA.gpi

out.tabfun.sin.txt out.fitfun.sin.txt: mainA.exe
	mono $< 

mainA.exe: mainA.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../../8-multimin/qnewton.cs
SRC +=../interpolater.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg Out* *.txt
 