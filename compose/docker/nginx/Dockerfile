FROM nginx:1.16.1

COPY nginx.conf /etc/nginx/nginx.conf
COPY localhost.crt /etc/ssl/certs/localhost.crt
COPY localhost.key /etc/ssl/private/localhost.key

# docker build -t nginx-reverseproxy -f nginx.Dockerfile .
# docker run -d -p 80:80 --name nginx-reverseproxy  nginx-reverseproxy
