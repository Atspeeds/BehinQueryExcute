
    const keywords = ["SELECT", "FROM", "WHERE", "INSERT", "UPDATE", "DELETE", "JOIN", "AND", "OR", "LEFT", "RIGHT", "INNER", "OUTER", "GROUP", "HAVING", "ORDER", "BY", "DISTINCT", "UNION", "NULL", "LIKE", "BETWEEN"];
    const functions = ["GETDATE()", "ISNULL()", "LEN()", "COUNT()", "MAX()", "MIN()", "AVG()", "SUM()"];

    function highlightSQL() {
        const input = document.getElementById("sqlInput").value;
    const output = document.getElementById("sqlOutput");


    const words = input.split(/(\s+|,|;|\(|\))/);
    let highlightedText = "";

        words.forEach(word => {
            const trimmedWord = word.trim();
    if (keywords.includes(trimmedWord.toUpperCase())) {
        highlightedText += '<span class="sql-keyword">' + word + '</span>';
            } else if (functions.some(func => trimmedWord.startsWith(func.replace("()", "")))) { // Check if it starts with any function name
        highlightedText += '<span class="sql-function">' + word + '</span>';
            } else if (trimmedWord === "") {
        highlightedText += " ";
            } else {
        highlightedText += word; 
            }
        });

    output.innerHTML = highlightedText;
    }
