import requests
from redis import StrictRedis
import itertools
import random
import datetime


class RequestGenerator(object):
    
    endpoints = [
        ('/api/users', 0.1),
        ('/api/authentication', 0.2),
        ('/api/posts', 0.3),
        ('/api/logout', 0.4),
    ]

    def __init__(self, uri):
        self.uri = uri
        self.probabilities = [p for e,p in self.endpoints]
        self.request_counts = [0] * len(self.endpoints)

    def generate_random_endpoint(self):
        totals = list(itertools.accumulate(self.probabilities))

        n = random.uniform(0, totals[-1])
        for i, total in enumerate(totals):
            if n <= total:
                return i

    def make_random_requests(self, num_requests):
        i=0
        while i < num_requests:
            random_endpoint = self.generate_random_endpoint()
            random_uri = self.endpoints[random_endpoint][0]
            sent_time = datetime.datetime.now()
            resp = requests.get(self.uri + random_uri)
            response_total_time = datetime.datetime.now() - sent_time
            response_code = resp.status_code
            self.request_counts[random_endpoint] += 1

            print("Endpoint: {}, Code: {}, total time: {}, total count: {}".format(self.endpoints[random_endpoint][0], response_code, response_total_time, self.request_counts[random_endpoint]))
            i += 1


if __name__ == '__main__':
    generator = RequestGenerator('http://45.79.134.56')

    generator.make_random_requests(1000)
