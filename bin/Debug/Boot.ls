(reference "System.Windows.Forms")
(reference "System.Drawing")
(reference "System.Xml")
(reference "LSharp.Libraries")

(using "System.Windows.forms")
(using "System.Drawing")
(using "System.xml")
(using "LSharp.Interpreter")
(using "LSharp.Libraries")

;;; Register LSharp.Libraries into the Environment
(call register container (environment))

(call Banner Program) ;;; Print the Banner from LSharp.Interpreter.Program

;;; Run lsi:

;;; Assign some variables
(= ? nil)
(= *error* nil) 

;;; Run an eternal loop
(while true
    (try 
        (do 
            (pr "> ") ;;; Write a prompt
            (= o (eval (read (get_in console)))) ;;; evaluate input
            (= ? o)
            (prl (call WriteToString Printer o)) ;;; write the return value
        )
        (do ;;; an error occurred
            (prl it) ;;; notify user
            (= *error* it) ;; save it in a variable
        )
    )
)
;;; the only way that this is come into is if you can exit this 
;;; eternal while without calling (exit) or something of that 
;;; sort. TODO: test if anything can happen after eternal loops
