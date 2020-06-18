all: Document.pdf

Document.pdf: doc.pdf
	mv doc.pdf Document.pdf

doc.pdf: doc.tex plotCircle.tex plotSin.tex plotSphere.tex plotHard.tex
	pdflatex  doc >>log
	pdflatex  doc >>log 

plotSphere.tex: plotSphere.gpi 		out.plot.PHL.Sphere.data
	gnuplot plotSphere.gpi >> log

plotHard.tex: plotHard.gpi 			out.plot.PHL.InverseCos.data
	gnuplot plotHard.gpi >> log

plotCircle.tex log: plotCircle.gpi 	out.plot.PHL.Circle.data
	gnuplot plotCircle.gpi >> log

plotSin.tex log: plotSin.gpi 		out.plot.PHL.SimpleSin.data
	gnuplot plotSin.gpi >> log


out.plot.PHL.Sphere.data out.plot.PHL.Circle.data out.plot.PHL.InverseCos.data out.plot.PHL.SimpleSin.data: main-plotgenerator.exe 
	mono $<

main-plotgenerator.exe: main-plotgenerator.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@


test: out_test.txt
	cat test_out.txt
out_test.txt: main-test.exe
	mono main-test.exe >$@
main-test.exe: main-test.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=plainmontecarlo.cs
SRC +=quasi-random-montecarlo.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@


	
clean:
	$(RM) out* *.exe *.dll *.svg *.log *.aux *.pdf *.dvi *.eps plot*.tex *.out *.fls *.log *.fdb_latexmk log *.data

cleanFigsAndDocument:
	$(RM) *.svg *.log *.aux *.pdf *.dvi *.eps *.out *.fls *.log *.fdb_latexmk plot*.tex log 

