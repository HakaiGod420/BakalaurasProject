/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './src/**/*.{js,jsx,ts,tsx}',
],
  theme: {
    extend: {},
  },
  daisyui: {
    themes: [
      {
        mytheme: {
        
"primary": "#16a34a",
        
"secondary": "#a3f7c0",
        
"accent": "#e88114",
        
"neutral": "#111827",
        
"base-100": "#1f2937",
        
"info": "#7EC0F6",
        
"success": "#1DD787",
        
"warning": "#D7B004",
        
"error": "#EC776F",
        },
      },
    ],
  },
  plugins: [require("daisyui")]
}
