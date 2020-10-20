# AnalyticsUnlimited
This project includes classes to fetch financial data from any global stock market and indian mutual funds. 
It provides set of API that will let you 
    1. Search a stock from any global stock exchange, any global market index or mutual fund from India
    2. Get real-time market data including quote for specific stock
    3. Get historical market data for specific stock or market index
    4. Get historical or current NAV for specific Mutual Fund
    5. You can fetch historical data based on date range
    6. For selected stocks it provides historical data for ALL technical indicators

Development platform:
  1. Microsoft .Net framework 4.8
  2. C#

Dependencies:
  1. Requires Newtonsoft JSON package added as reference

Required folder:
  1. Each method will fetch real-time data from market
  2. It will save the fetched data to supplied folder
All API methods are exposed as static methods.

USAGE: Please refer to "Analytics" project which is using the API to fetch data
