all: out.txt plotB.svg
	cat out.txt

plotB.svg: plotB.gpi out.plot.txt
	gnuplot $<
	
out.txt out.plot.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC += ../../2-linear-equations/gr.gs.cs

matlib.dll: least.squares.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.txt *.exe *.dll *.svg
