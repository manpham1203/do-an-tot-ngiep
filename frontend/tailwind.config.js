module.exports = {
  darkMode: 'class',
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  plugins: [],
  important: false,
  theme: {
    extend: {
      fontFamily: {
        main: ["Montserrat Alternates", "sans-serif"],
        second: ["Montserrat", "sans-serif"],
      },
      backgroundColor: {
        "custom-color-001": "rgba(201, 76, 76, 1)",
        sidebar: "#343A40",
        "main-color": "#CC9966",
        success: "#28A745",
        danger: "#DC3545",
        warning: "#FFC107",
        "second":"#202121",
        "third":"#fcfcfc",
        'darkMode':'#3A3B3C'
      },
      colors: {
        "dark-purple": "#081A51",
        "light-white": "rgba(255,255,255,0.17)",
        "input-color": "#202124",
        "input-label": "#5F6368",
        "main-color": "#CC9966",
        success: "#28A745",
        danger: "#DC3545",
        warning: "#FFC107",
        cancel: "##6C757D",
        submit:"#1A56DB",
        "second":"#202121",
        "third":"#fcfcfc",
        'darkMode':'#3A3B3C'
      },
      borderColor: {
        "input-border": "#DADCE0",
        "main-color": "#CC9966",
        success: "#28A745",
        danger: "#DC3545",
        warning: "#FFC107",
        "second":"#202121",
        "third":"#fcfcfc",
        'darkMode':'#3A3B3C'
      },
      boxShadow: {
        admin: "rgba(0, 0, 0, 0.24) 0px 3px 8px",
      },
    },
  },
  variants: {
    extend: {
      display: ["group-hover"],
      margin: ["group-hover"],
      visibility: ["group-hover"],
    },
  },
};
