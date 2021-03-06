﻿using Foundations.Logger;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace DcmSMService.WebAPIClient
{
    class GithubRepositoryRequest
    {
        private const string TAG = "GithubRepositoryRequest";
        private static readonly HttpClient client = new HttpClient();

        public static void Request()
        {
            var repositories = ProcessRepositories().Result;

            foreach (var repo in repositories)
            {
                Log.Debug(TAG, repo.Name);
                Log.Debug(TAG, repo.Description);

                if (repo.GitHubHomeUrl != null)
                {
                    Log.Debug(TAG, repo.GitHubHomeUrl.ToString());
                }

                if (repo.Homepage != null)
                {
                    Log.Debug(TAG, repo.Homepage.ToString());
                }

                Log.Debug(TAG, repo.Watchers.ToString());

                if (repo.LastPush != null)
                {
                    Log.Debug(TAG, repo.LastPush.ToString());
                }
                
                Log.Debug(TAG, Environment.NewLine);
            }
        }

        private static async Task<List<GithubRepositoryModel>> ProcessRepositories()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<GithubRepositoryModel>));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/users/haeram27/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<GithubRepositoryModel>;
            return repositories;
        }
    }
}
